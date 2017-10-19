using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompensationCalculator
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void SalaryButton_Click(object sender, EventArgs e)
		{
			var scenario = CompensationScenario.TypicalFullTimeJob();
			var result = scenario.CalculateSalary(SalaryBox.Value);
			var check = scenario.SalaryRequired(result.TotalCompensation, 0) == SalaryBox.Value;

			var lines = new List<string>
			{
				$"Result of Compensation Scenario: Typical Full Type Salary Job",
				$"Result Checked: {check}",
				$"Salary: {SalaryBox.Value:c}",
				$"Hours Worked: {result.ActualHoursWorked:n0}",
				$"Hours Off: {scenario.DaysOffPerYear * 8:n0}",
				$"Federal Tax Rate: {scenario.TaxProfile.TaxBracketRate:p2}",
				$"Social Security Tax Rate: {scenario.TaxProfile.SocialSecurityRate:p2}",
				$"Medicare Tax Rate: {scenario.TaxProfile.MedicareRate:p2}",
				$"Taxes Paid By Employer: {result.TaxesPaidByEmployer:c}",
				$"Taxes Paid By Employee: {result.TaxesPaidByEmployee:c}",
				$"Total Taxes Paid: {result.TotalTaxPaid:c}",
				$"Cash Compensation: {result.NetCompensation:c}",
				$"Employer Retirement Contribution: {result.RetirementContributionByEmployer:c}",
				$"Total Retirement Contribution: {result.TotalRetirementContribution:c}",
				$"Health and Dental: {result.HealthAndDentalPaidByEmployer:c}",
				$"Total Benefits: {result.TotalBenefitsPaidByEmployer:c}",
				$"Total Compensation: {result.TotalCompensation:c}",
				$"Compensation per Hour: {result.CompensationPerHour:c}",
				$"Equivalent Hourly Wage: {result.EquivalentWageRate:c}",
				$"Equivalent Contract Rate: {result.EquivalentContractRate:c}"
			};
			OutputBox.Lines = lines.ToArray();
		}

		private void WageButton_Click(object sender, EventArgs e)
		{
			var scenario = CompensationScenario.TypicalFullTimeJob();
			var result = scenario.CalculateHourlyWage(WageBox.Value);
			var check = scenario.WageRequired(result.TotalCompensation) == WageBox.Value;

			var lines = new List<string>
			{
				$"Result of Compensation Scenario: Typical Full Type Hourly Rate Job",
				$"Result Checked: {check}",
				$"Hourly Rate: {WageBox.Value:c}",
				$"Hours Worked: {result.ActualHoursWorked:n0}",
				$"Hours Off: {scenario.DaysOffPerYear * 8:n0}",
				$"Federal Tax Rate: {scenario.TaxProfile.TaxBracketRate:p2}",
				$"Social Security Tax Rate: {scenario.TaxProfile.SocialSecurityRate:p2}",
				$"Medicare Tax Rate: {scenario.TaxProfile.MedicareRate:p2}",
				$"Taxes Paid By Employer: {result.TaxesPaidByEmployer:c}",
				$"Taxes Paid By Employee: {result.TaxesPaidByEmployee:c}",
				$"Total Taxes Paid: {result.TotalTaxPaid:c}",
				$"Cash Compensation: {result.NetCompensation:c}",
				$"Employer Retirement Contribution: {result.RetirementContributionByEmployer:c}",
				$"Total Retirement Contribution: {result.TotalRetirementContribution:c}",
				$"Health and Dental: {result.HealthAndDentalPaidByEmployer:c}",
				$"Total Benefits: {result.TotalBenefitsPaidByEmployer:c}",
				$"Total Compensation: {result.TotalCompensation:c}",
				$"Compensation per Hour: {result.CompensationPerHour:c}",
				$"Equivalent Salary: {result.EquivalentSalary:c}",
				$"Equivalent Contract Rate: {result.EquivalentContractRate:c}"
			};
			OutputBox.Lines = lines.ToArray();
		}

		private void ContractButton_Click(object sender, EventArgs e)
		{
			var scenario = CompensationScenario.TypicalFullTimeJob();
			var result = scenario.CalculateContractRate(ContractBox.Value);
			var check = scenario.ContractRateRequired(result.TotalCompensation, result.ActualHoursWorked) == ContractBox.Value;

			var lines = new List<string>
			{
				$"Result of Compensation Scenario: Typical Full Type Hourly Rate Job",
				$"Result Checked: {check}",
				$"Hourly Rate: {ContractBox.Value:c}",
				$"Hours Worked: {result.ActualHoursWorked:n0}",
				$"Hours Off: {scenario.DaysOffPerYear * 8:n0}",
				$"Federal Tax Rate: {scenario.TaxProfile.TaxBracketRate:p2}",
				$"Social Security Tax Rate: {scenario.TaxProfile.SocialSecurityRate:p2}",
				$"Medicare Tax Rate: {scenario.TaxProfile.MedicareRate:p2}",
				$"Taxes Paid By Employer: {result.TaxesPaidByEmployer:c}",
				$"Taxes Paid By Employee: {result.TaxesPaidByEmployee:c}",
				$"Total Taxes Paid: {result.TotalTaxPaid:c}",
				$"Cash Compensation: {result.NetCompensation:c}",
				$"Employer Retirement Contribution: {result.RetirementContributionByEmployer:c}",
				$"Total Retirement Contribution: {result.TotalRetirementContribution:c}",
				$"Health and Dental: {result.HealthAndDentalPaidByEmployer:c}",
				$"Total Benefits: {result.TotalBenefitsPaidByEmployer:c}",
				$"Total Compensation: {result.TotalCompensation:c}",
				$"Compensation per Hour: {result.CompensationPerHour:c}",
				$"Equivalent Salary: {result.EquivalentSalary:c}",
				$"Equivalent Hourly Wage: {result.EquivalentWageRate:c}",
			};
			OutputBox.Lines = lines.ToArray();
		}
	}
}
