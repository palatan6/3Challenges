using System;
using System.Collections.Generic;

namespace ChallengesTestApp.Services;

public interface ISalesDataService
{
	const string SourceFileFilters = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
	const float TaxRateDefaultValue = 8.625f;

	IEnumerable<object> GetSalesFromFile(string path);

	float CalculateTax(DateTime selectedDate, float taxRate);
}