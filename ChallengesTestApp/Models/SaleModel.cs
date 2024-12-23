using System;
using FileHelpers;

namespace ChallengesTestApp.Models;

[DelimitedRecord(",")]
public class SaleModel
{
	[FieldConverter(ConverterKind.Date, "dd-MM-yyyy")]
	public DateTime Date { get; set; }

	public string SaleName { get; set; }

	public float Price { get; set; }
}