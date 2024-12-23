using System;
using System.Collections.Generic;
using System.Linq;
using ChallengesTestApp.Models;
using FileHelpers;

namespace ChallengesTestApp.Services;

public class SalesDataService : ISalesDataService
{
	private readonly FileHelperEngine<SaleModel> _engine = new();

	private readonly List<SaleModel> _salesModels = new();

	public IEnumerable<object> GetSalesFromFile(string path)
	{
		_salesModels.AddRange(_engine.ReadFile(path));

		return _salesModels;
	}

	public float CalculateTax(DateTime selectedDate, float taxRate)
	{
		var totalPrice = _salesModels
			.Where(s => s.Price > 0 && s.Date.Year == selectedDate.Year && s.Date.Month == selectedDate.Month)
			.Sum(s => s.Price);

		return totalPrice * taxRate / 100;
	}
}