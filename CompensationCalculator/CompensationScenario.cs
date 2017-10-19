using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompensationCalculator
{
	public class CompensationScenario
	{
		private static readonly decimal _weeksPerYear = 365.25m / 7;

		public int DaysOffPerYear { get; private set; }

		public TaxProfile TaxProfile { get; private set; }

		public BenefitsProfile BenefitsProfile { get; private set; }

		public CompensationScenario(int daysOffPerYear, TaxProfile taxProfile, BenefitsProfile benefitsProfile)
		{
			DaysOffPerYear = daysOffPerYear;
			TaxProfile = taxProfile;
			BenefitsProfile = benefitsProfile;
		}

		public static CompensationScenario TypicalFullTimeJob()
			=> new CompensationScenario(25, TaxProfile.Current(), BenefitsProfile.Typical());

		public static CompensationScenario Current()
			=> new CompensationScenario(25, TaxProfile.Current(), BenefitsProfile.None());

		public SalaryCalculationResult CalculateSalary(decimal salary, decimal bonus = 0)
		{
			if (salary < 1)
				throw new Exception("Salary too low to calculate.");

			var hoursAvailable = 40 * _weeksPerYear;
			var hoursOff = 8 * DaysOffPerYear;
			var hoursWorked = hoursAvailable - hoursOff;
			var hoursPaid = hoursAvailable;

			var taxableAmount = salary + bonus;
			var federalTax = TaxProfile.FixedBaseAmount + TaxProfile.TaxBracketRate * (taxableAmount - TaxProfile.TaxBracketThreshold);
			var employerTax = (TaxProfile.MedicareRate + TaxProfile.SocialSecurityRate) * taxableAmount;
			var employeeTax = federalTax + employerTax;

			var employerRetirement = Math.Min(BenefitsProfile.RetirementContributionRate, BenefitsProfile.RetirementMatchingContributionRate) * salary;
			var employeeRetirement = BenefitsProfile.RetirementContributionRate * salary;

			var result = new SalaryCalculationResult
			{
				ActualHoursWorked = hoursWorked,
				CashCompensation = taxableAmount,
				TaxesPaidByEmployer = employerTax,
				TaxesPaidByEmployee = employeeTax,
				RetirementContributionByEmployer = employerRetirement,
				RetirementContributionByEmployee = employeeRetirement,
				HealthAndDentalPaidByEmployer = BenefitsProfile.EmployerHealthAndDentalCostsPaid
			};

			result.EquivalentContractRate = ContractRateRequired(result.TotalCompensation, result.ActualHoursWorked);
			result.EquivalentWageRate = WageRequired(result.TotalCompensation);
			return result;
		}

		public decimal SalaryRequired(decimal totalCompensation, decimal bonus = 0)
		{
			/*
				For salary, total compensation includes bonus and benefits.
				Total Compensation
					= salary
					+ bonus
					+ (TaxProfile.MedicareRate + TaxProfile.SocialSecurityRate) * wagesPerYear 
					+ Math.Min(BenefitsProfile.RetirementContributionRate, BenefitsProfile.RetirementMatchingContributionRate) * wagesPerYear 
					+ BenefitsProfile.EmployerHealthAndDentalCostsPaid;
				C = s + b + (TM + TS)(s + b) + rs + BE = (1 + TM + TS + r)s + (TM + TS)b + (TM)(TS) + BE
				s = (C - BE - (TM + TS)b) / (1 + TM + TS + r)
			*/
			var r = Math.Min(BenefitsProfile.RetirementContributionRate, BenefitsProfile.RetirementMatchingContributionRate);
			var num = totalCompensation - BenefitsProfile.EmployerHealthAndDentalCostsPaid - bonus * (TaxProfile.MedicareRate + TaxProfile.SocialSecurityRate);
			var den = 1 + TaxProfile.MedicareRate + TaxProfile.SocialSecurityRate + r;
			return num / den; // Paid for Time Off so No Adjustment for Hours
		}

		public WageCalculationResult CalculateHourlyWage(decimal wagesPerHour)
		{
			if (wagesPerHour < 1)
				throw new Exception("Wages too low to calculate.");

			var hoursAvailable = 40 * _weeksPerYear;
			var hoursOff = 8 * DaysOffPerYear;
			var hoursWorked = hoursAvailable - hoursOff;
			var hoursPaid = hoursAvailable;

			var wagesPerYear = hoursPaid * wagesPerHour; 
			var taxableAmount = wagesPerYear;
			var federalTax = TaxProfile.FixedBaseAmount + TaxProfile.TaxBracketRate * (taxableAmount - TaxProfile.TaxBracketThreshold);
			var employerTax = (TaxProfile.MedicareRate + TaxProfile.SocialSecurityRate) * taxableAmount;
			var employeeTax = federalTax + employerTax;

			var employerRetirement = Math.Min(BenefitsProfile.RetirementContributionRate, BenefitsProfile.RetirementMatchingContributionRate) * wagesPerYear;
			var employeeRetirement = BenefitsProfile.RetirementContributionRate * wagesPerYear;

			var result = new WageCalculationResult
			{
				ActualHoursWorked = 40 * _weeksPerYear - 8 * DaysOffPerYear,
				CashCompensation = taxableAmount,
				TaxesPaidByEmployer = employerTax,
				TaxesPaidByEmployee = employeeTax,
				RetirementContributionByEmployer = employerRetirement,
				RetirementContributionByEmployee = employeeRetirement,
				HealthAndDentalPaidByEmployer = BenefitsProfile.EmployerHealthAndDentalCostsPaid
			};

			result.EquivalentSalary = SalaryRequired(result.TotalCompensation, 0);
			result.EquivalentContractRate = ContractRateRequired(result.TotalCompensation, result.ActualHoursWorked);
			return result;
		}

		public decimal WageRequired(decimal totalCompensation)
		{
			/*
				For wages, total compensation includes benefits.
				Total Compensation
					= wagesPerYear 
					+ (TaxProfile.MedicareRate + TaxProfile.SocialSecurityRate) * wagesPerYear 
					+ Math.Min(BenefitsProfile.RetirementContributionRate, BenefitsProfile.RetirementMatchingContributionRate) * wagesPerYear 
					+ BenefitsProfile.EmployerHealthAndDentalCostsPaid;
				C = w + (TM + TS)w + rw + BE = (1 + TM + TS + r)w + BE
				w = (C - BE) / (1 + TM + TS + r)
			*/
			var contributionRate = Math.Min(BenefitsProfile.RetirementContributionRate, BenefitsProfile.RetirementMatchingContributionRate);
			var wagesPerYear = (totalCompensation - BenefitsProfile.EmployerHealthAndDentalCostsPaid)
				/ (1 + TaxProfile.MedicareRate + TaxProfile.SocialSecurityRate + contributionRate);

			return wagesPerYear / (40 * _weeksPerYear); // Paid for Time Off
		}

		public ContractCalculationResult CalculateContractRate(decimal ratePerHour)
		{
			if (ratePerHour < 1)
				throw new Exception("Rate too low to calculate.");

			var hoursAvailable = 40 * _weeksPerYear;
			var hoursOff = 8 * DaysOffPerYear;
			var hoursWorked = hoursAvailable - hoursOff;
			var hoursPaid = hoursWorked;

			var totalMoneyPaid = hoursPaid * ratePerHour;
			var taxableAmount = totalMoneyPaid;
			var federalTax = TaxProfile.FixedBaseAmount + TaxProfile.TaxBracketRate * (taxableAmount - TaxProfile.TaxBracketThreshold);
			var employerTax = 0;
			var employeeTax = federalTax + 2 * (TaxProfile.MedicareRate + TaxProfile.SocialSecurityRate) * taxableAmount;

			var employerRetirement =0;
			var employeeRetirement = BenefitsProfile.RetirementContributionRate * totalMoneyPaid;

			var result = new ContractCalculationResult
			{
				ActualHoursWorked = 40 * _weeksPerYear - 8 * DaysOffPerYear,
				CashCompensation = taxableAmount,
				TaxesPaidByEmployer = employerTax,
				TaxesPaidByEmployee = employeeTax,
				RetirementContributionByEmployer = employerRetirement,
				RetirementContributionByEmployee = employeeRetirement,
				HealthAndDentalPaidByEmployer = 0
			};

			result.EquivalentSalary = SalaryRequired(result.TotalCompensation, 0);
			result.EquivalentWageRate = WageRequired(result.TotalCompensation);
			return result;
		}

		public decimal ContractRateRequired(decimal totalCompensation, decimal hoursWorked)
		{
			/*
				For contract rate, total compensation *does not* include bonus or benefits and must be adjusted for hours worked
				Total Compensation = hoursPaid * ratePerHour
				C = h * r
				r = C / h
			*/
			return totalCompensation / hoursWorked;
		}
	}

	public class TaxProfile
	{
		public decimal FixedBaseAmount { get; set; }
		public decimal TaxBracketThreshold { get; set; }
		public decimal TaxBracketRate { get; set; }
		public decimal SocialSecurityRate { get; set; }
		public decimal MedicareRate { get; set; }
		public static TaxProfile Current()
		{
			return new TaxProfile
			{
				FixedBaseAmount = 1865m,
				TaxBracketThreshold = 18650,
				TaxBracketRate = 0.15m, // employee only
				SocialSecurityRate = .062m, // each
				MedicareRate = .0145m // each
			};
		}

		[Obsolete]
		public decimal CalculateEmployerTaxOnAmount(decimal amount, TimePeriod period = null)
		{
			var factor = period == null ? 1m : period.Factor;
			var annualAmount = amount * factor;
			var annualTax = annualAmount * (SocialSecurityRate + MedicareRate);
			return annualTax / factor;
		}

		[Obsolete]
		public decimal CalculateAmountFromEmployerTax(decimal tax, TimePeriod period = null)
		{
			var factor = period == null ? 1m : period.Factor;
			var annualTax = tax * factor;
			var annualAmount = annualTax / (SocialSecurityRate + MedicareRate);
			return annualAmount / factor;
		}

		[Obsolete]
		public decimal CalculateEmployeeTaxOnAmount(decimal amount, TimePeriod period = null)
		{
			var factor = period == null ? 1m : period.Factor;
			var annualAmount = amount * factor;
			var federalTax = FixedBaseAmount + TaxBracketRate * (annualAmount - TaxBracketThreshold);
			var annualTax = federalTax + annualAmount * (SocialSecurityRate + MedicareRate);
			return annualTax / factor;
		}
	}

	public class BenefitsProfile
	{
		public decimal RetirementContributionRate { get; set; }

		public decimal RetirementMatchingContributionRate { get; set; }

		public decimal EmployerHealthAndDentalCostsPaid { get; set; }

		public static BenefitsProfile Typical() => new BenefitsProfile { RetirementContributionRate = .03m, RetirementMatchingContributionRate = .03m, EmployerHealthAndDentalCostsPaid = 1000 };

		public static BenefitsProfile None() => new BenefitsProfile { RetirementContributionRate = 0m, RetirementMatchingContributionRate = 0m, EmployerHealthAndDentalCostsPaid = 0 };

		[Obsolete]
		public decimal EmployerCost(decimal onPayAmount, TimePeriod period = null)
		{
			var factor = period == null ? 1m : period.Factor;
			var annualPayAmount = onPayAmount * factor;
			var annualBenefits = RetirementMatchingContributionRate * annualPayAmount + EmployerHealthAndDentalCostsPaid;
			return annualBenefits / factor;
		}
		[Obsolete]
		public decimal RetirementContributionByEmployer(decimal onPayAmount, TimePeriod period = null)
		{
			var factor = period == null ? 1m : period.Factor;
			var annualPayAmount = onPayAmount * factor;
			var rate = Math.Min(RetirementContributionRate, RetirementMatchingContributionRate);
			var annualBenefits = rate * annualPayAmount;
			return annualBenefits / factor;
		}
		[Obsolete]
		public decimal RetirementContributionByEmployee(decimal onPayAmount, TimePeriod period = null)
		{
			var factor = period == null ? 1m : period.Factor;
			var annualPayAmount = onPayAmount * factor;
			var annualBenefits = RetirementContributionRate * annualPayAmount;
			return annualBenefits / factor;
		}
	}

	public class TimePeriod
	{
		private TimePeriod(decimal factor) { Factor = factor; }

		public static TimePeriod Yearly { get; } = new TimePeriod(1);

		public static TimePeriod Monthly { get; } = new TimePeriod(12);

		public static TimePeriod Weekly { get; } = new TimePeriod(365.25m / 7m);

		public static TimePeriod Daily { get; } = new TimePeriod(365.25m);

		public decimal Factor { get; private set; }

		public static decimal Convert(decimal amount, TimePeriod from, TimePeriod to) => amount * from.Factor / to.Factor;
	}

	public class CalculationResult
	{
		public decimal ActualHoursWorked { get; set; }
		public decimal CashCompensation { get; set; }
		public decimal TaxesPaidByEmployer { get; set; }
		public decimal TaxesPaidByEmployee { get; internal set; }
		public decimal RetirementContributionByEmployer { get; set; }
		public decimal RetirementContributionByEmployee { get; set; }
		public decimal HealthAndDentalPaidByEmployer { get; internal set; }
		public decimal TotalRetirementContribution => RetirementContributionByEmployer + RetirementContributionByEmployee;
		public decimal TotalBenefitsPaidByEmployer => HealthAndDentalPaidByEmployer + RetirementContributionByEmployer;
		public decimal TotalTaxPaid => TaxesPaidByEmployer + TaxesPaidByEmployee;
		public decimal TotalCompensation => CashCompensation + TaxesPaidByEmployer + TotalBenefitsPaidByEmployer;
		public decimal NetCompensation => CashCompensation - TaxesPaidByEmployee - RetirementContributionByEmployee;
		public decimal CompensationPerHour => TotalCompensation / ActualHoursWorked;
	}

	public class SalaryCalculationResult : CalculationResult
	{
		public decimal EquivalentWageRate { get; set; }
		public decimal EquivalentContractRate { get; set; }
	}

	public class WageCalculationResult : CalculationResult
	{
		public decimal EquivalentContractRate { get; set; }
		public decimal EquivalentSalary { get; set; }
	}

	public class ContractCalculationResult : CalculationResult
	{
		public decimal EquivalentWageRate { get; set; }
		public decimal EquivalentSalary { get; set; }
	}
}
