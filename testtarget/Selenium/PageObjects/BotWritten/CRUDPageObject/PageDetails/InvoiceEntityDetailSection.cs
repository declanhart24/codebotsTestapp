/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 * 
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */
using System;
using System.Linq;
using System.Collections.Generic;
using APITests.EntityObjects.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.PageObjects.Components;
using SeleniumTests.Setup;
using SeleniumTests.Utils;
using SeleniumTests.Enums;
using SeleniumTests.PageObjects.BotWritten;
// % protected region % [Custom imports] off begin
// % protected region % [Custom imports] end

namespace SeleniumTests.PageObjects.CRUDPageObject.PageDetails
{
	//This section is a mapping from an entity object to an entity create or detailed view page
	public class InvoiceEntityDetailSection : BasePage, IEntityDetailSection
	{
		private readonly IWait<IWebDriver> _driverWait;
		private readonly IWebDriver _driver;
		private readonly bool _isFastText;
		private readonly ContextConfiguration _contextConfiguration;

		// reference elements

		//FlatPickr Elements

		//Attribute Headers
		private readonly InvoiceEntity _invoiceEntity;

		//Attribute Header Titles
		private IWebElement IdHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='ID']"));
		private IWebElement InvoicenumberHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='InvoiceNumber']"));
		private IWebElement InvoicetotalHeaderTitle => _driver.FindElementExt(By.XPath("//th[text()='InvoiceTotal']"));

		// Datepickers
		public IWebElement CreateAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.created > input[type='date']"));
		public IWebElement ModifiedAtDatepickerField => _driver.FindElementExt(By.CssSelector("div.modified > input[type='date']"));

		public InvoiceEntityDetailSection(ContextConfiguration contextConfiguration, InvoiceEntity invoiceEntity = null) : base(contextConfiguration)
		{
			_driver = contextConfiguration.WebDriver;
			_driverWait = contextConfiguration.WebDriverWait;
			_isFastText = contextConfiguration.SeleniumSettings.FastText;
			_contextConfiguration = contextConfiguration;
			_invoiceEntity = invoiceEntity;

			InitializeSelectors();
			// % protected region % [Add any extra construction requires] off begin
			// % protected region % [Add any extra construction requires] end
		}

		// initialise all selectors and grouping them with the selector type which is used
		private void InitializeSelectors()
		{
			// Attribute web elements
			selectorDict.Add("IdElement", (selector: "//div[contains(@class, 'id')]//input", type: SelectorType.XPath));
			selectorDict.Add("InvoicenumberElement", (selector: "//div[contains(@class, 'invoicenumber')]//input", type: SelectorType.XPath));
			selectorDict.Add("InvoicetotalElement", (selector: "//div[contains(@class, 'invoicetotal')]//input", type: SelectorType.XPath));

			// Reference web elements

			// Datepicker
			selectorDict.Add("CreateAtDatepickerField", (selector: "//div[contains(@class, 'created')]/input", type: SelectorType.XPath));
			selectorDict.Add("ModifiedAtDatepickerField", (selector: "//div[contains(@class, 'modified')]/input", type: SelectorType.XPath));
		}

		//outgoing Reference web elements

		//Attribute web Elements
		private IWebElement IdElement => FindElementExt("IdElement");
		private IWebElement InvoicenumberElement => FindElementExt("InvoicenumberElement");
		private IWebElement InvoicetotalElement => FindElementExt("InvoicetotalElement");

		// Return an IWebElement that can be used to sort an attribute.
		public IWebElement GetHeaderTile(string attribute)
		{
			return attribute switch
			{
				"ID" => IdHeaderTitle,
				"InvoiceNumber" => InvoicenumberHeaderTitle,
				"InvoiceTotal" => InvoicetotalHeaderTitle,
				_ => throw new Exception($"Cannot find header tile {attribute}"),
			};
		}

		// Return an IWebElement for an attribute input
		public IWebElement GetInputElement(string attribute)
		{
			switch (attribute)
			{
				case "ID":
					return IdElement;
				case "InvoiceNumber":
					return InvoicenumberElement;
				case "InvoiceTotal":
					return InvoicetotalElement;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		public void SetInputElement(string attribute, string value)
		{
			switch (attribute)
			{
				case "ID":
					SetId(int.Parse(value));
					break;
				case "InvoiceNumber":
					SetInvoicenumber(value);
					break;
				case "InvoiceTotal":
					SetInvoicetotal(Convert.ToDouble(value));
					break;
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private By GetErrorAttributeSectionAsBy(string attribute)
		{
			return attribute switch
			{
				"ID" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.id > div > p"),
				"InvoiceNumber" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.invoicenumber > div > p"),
				"InvoiceTotal" => WebElementUtils.GetElementAsBy(SelectorPathType.CSS, "div.invoicetotal > div > p"),
				_ => throw new Exception($"No such attribute {attribute}"),
			};
		}

		public List<string> GetErrorMessagesForAttribute(string attribute)
		{
			var elementBy = GetErrorAttributeSectionAsBy(attribute);
			WaitUtils.elementState(_driverWait, elementBy, ElementState.VISIBLE);
			var element = _driver.FindElementExt(elementBy);
			var errors = new List<string>(element.Text.Split("\r\n"));
			// remove the item in the list which is the name of the attribute and not an error.
			errors.Remove(attribute);
			return errors;
		}

		public InvoiceEntity ExtractEntity()
		{
			var invoiceEntity = new InvoiceEntity
			{
				Id = GetId,
				Invoicenumber = GetInvoicenumber,
				Invoicetotal = GetInvoicetotal,
			};

			// % protected region % [Add any extra steps to extract an entity] off begin
			// % protected region % [Add any extra steps to extract an entity] end

			return invoiceEntity;
		}

		public void Apply()
		{
			// % protected region % [Configure entity application here] off begin
			SetId(_invoiceEntity.Id);
			SetInvoicenumber(_invoiceEntity.Invoicenumber);
			SetInvoicetotal(_invoiceEntity.Invoicetotal);

			// % protected region % [Configure entity application here] end
		}

		public List<Guid> GetAssociation(string referenceName)
		{
			switch (referenceName)
			{
				default:
					throw new Exception($"Cannot find association type {referenceName}");
			}
		}

		// set associations

		// get associations

		// wait for dropdown to be displaying options
		private void WaitForDropdownOptions()
		{
			var xpath = "//*/div[@aria-expanded='true']";
			var elementBy = WebElementUtils.GetElementAsBy(SelectorPathType.XPATH, xpath);
			WaitUtils.elementState(_driverWait, elementBy,ElementState.EXISTS);
		}

		private void SetId (int? value)
		{
			if (value is int intValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "id", intValue.ToString(), _isFastText);
			}
		}

		private int? GetId =>
			int.Parse(IdElement.Text);

		private void SetInvoicenumber (String value)
		{
			TypingUtils.InputEntityAttributeByClass(_driver, "invoicenumber", value, _isFastText);
			InvoicenumberElement.SendKeys(Keys.Tab);
			InvoicenumberElement.SendKeys(Keys.Escape);
		}

		private String GetInvoicenumber =>
			InvoicenumberElement.Text;

		private void SetInvoicetotal (Double? value)
		{
			if (value is double doubleValue)
			{
				TypingUtils.InputEntityAttributeByClass(_driver, "invoicetotal", doubleValue.ToString(), _isFastText);
			}
		}

		private Double? GetInvoicetotal =>
			Convert.ToDouble(InvoicetotalElement.Text);

		// % protected region % [Add any additional getters and setters of web elements] off begin
		// % protected region % [Add any additional getters and setters of web elements] end
	}
}