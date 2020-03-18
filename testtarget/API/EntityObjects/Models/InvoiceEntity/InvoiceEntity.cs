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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityObject.Enums;
using APITests.DataFixtures;
using RestSharp;
using TestDataLib;
using Test.Utility;

namespace APITests.EntityObjects.Models
{
	public class InvoiceEntity : BaseEntity	{

		// 
		public int? Id { get; set; }
		// invoice number
		public String Invoicenumber { get; set; }
		// total of the invoice
		public Double? Invoicetotal { get; set; }

		public InvoiceEntity()
		{
			EntityName = "InvoiceEntity";

			InitialiseAttributes();
			InitialiseReferences();
		}

		public InvoiceEntity(ConfigureOptions option)
		{
			Configure(option);
			InitialiseAttributes();
			InitialiseReferences();
		}

		public override void Configure(ConfigureOptions option)
		{
			switch (option)
			{
				case ConfigureOptions.CREATE_ATTRIBUTES_AND_REFERENCES:
					SetValidEntityAttributes();
					SetValidEntityAssociations();
					break;
				case ConfigureOptions.CREATE_ATTRIBUTES_ONLY:
					SetValidEntityAttributes();
					break;
				case ConfigureOptions.CREATE_REFERENCES_ONLY:
					SetValidEntityAssociations();
					break;
				case ConfigureOptions.CREATE_INVALID_ATTRIBUTES:
					SetInvalidEntityAttributes();
					break;
				case ConfigureOptions.CREATE_INVALID_ATTRIBUTES_VALID_REFERENCES:
					SetInvalidEntityAttributes();
					SetValidEntityAssociations();
					break;
			}
		}

		private void InitialiseAttributes()
		{
			Attributes.Add(new Attribute
			{
				Name = "Id",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Invoicenumber",
				IsRequired = true
			});
			Attributes.Add(new Attribute
			{
				Name = "Invoicetotal",
				IsRequired = false
			});
		}

		private void InitialiseReferences()
		{
		}

		public override (int min, int max) GetLengthValidatorMinMax(string attribute)
		{
			switch(attribute)
			{
				default:
					throw new Exception($"{attribute} does not exist or does not have a length validator");
			}
		}

		public override string GetInvalidAttribute(string attribute, string validator)
		{
			switch (attribute)
			{
				case "ID":
					return GetInvalidId(validator);
				case "InvoiceNumber":
					return GetInvalidInvoicenumber(validator);
				default:
					throw new Exception($"Cannot find input element {attribute}");
			}
		}

		private static string GetInvalidId(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Id");
			}
		}
		private static string GetInvalidInvoicenumber(string validator)
		{
			switch (validator)
			{
					case "Required":
						return "";
				default:
					throw new Exception($"Cannot find validator {validator} for attribute Invoicenumber");
			}
		}


		/// <summary>
		/// Returns a list of invalid/mutated jsons and expected errors. The expected errors are the errors that
		/// should be returned when trying to use the invalid/mutated jsons in a create api request.
		/// </summary>
		/// <returns></returns>
		public override ICollection<(List<string> expectedErrors, JsonObject jsonObject)> GetInvalidMutatedJsons()
		{
			return new List<(List<string> expectedError, JsonObject jsonObject)>
			{

			(
				new List<string>
				{
					"The Id field is required.",
				},

				new JsonObject
				{
						["id"] = Id,
						// not defining id,
						["invoicenumber"] = Invoicenumber,
						["invoicetotal"] = Invoicetotal.ToString(),
				}
			),
			(
				new List<string>
				{
					"The Invoicenumber field is required.",
				},

				new JsonObject
				{
						["id"] = Id,
						// not defining invoicenumber,
						["id"] = Id.ToString(),
						["invoicetotal"] = Invoicetotal.ToString(),
				}
			),

			};
		}

		public override Dictionary<string, string> ToDictionary()
		{
			var entityVar = new Dictionary<string, string>()
			{
				{"id" , Id.ToString()},
				{"id" , Id.ToString()},
				{"invoicenumber" , Invoicenumber},
				{"invoicetotal" , Invoicetotal.ToString()},
			};


			return entityVar;
		}

		public override JsonObject ToJson()
		{
			var entityVar = new JsonObject
			{
				["id"] = Id,
				["id"] = Id,
				["invoicenumber"] = Invoicenumber.ToString(),
				["invoicetotal"] = Invoicetotal.ToString(),
			};


			return entityVar;
		}

		public override void SetReferences (Dictionary<string, ICollection<Guid>> entityReferences)
		{
			foreach (var (key, guidCollection) in entityReferences)
			{
				switch (key)
				{
					default:
						throw new Exception($"{key} not valid reference key");
				}
			}
		}

		private void SetOneReference (string key, Guid guid)
		{
			switch (key)
			{
				default:
					throw new Exception($"{key} not valid reference key");
			}
		}

		private void SetManyReference (string key, ICollection<Guid> guids)
		{
			switch (key)
			{
				default:
					throw new Exception($"{key} not valid reference key");
			}
		}

		public override List<Guid> GetManyToManyReferences (string reference)
		{
			switch (reference)
			{
				default:
					throw new Exception($"{reference} not valid many to many reference key");
			}
		}

		private List<JsonObject> FormatManyToManyJsonList(string key, List<Guid> values)
		{
			var manyToManyList = new List<JsonObject>();
			values?.ForEach(x => manyToManyList.Add(new JsonObject {[key] = x }));
			return manyToManyList;
		}


		// TODO needs some warning if trying to get an invalid entity, and the entity
		// attributes don't actually have any validators to violate.
		private void SetInvalidEntityAttributes()
		{
			// not defining Id
			// not defining Invoicenumber
		}

		/// <summary>
		/// Gets an entity that violates the validators of its attributes,
		/// if any attributes have a validator to violate.
		/// </summary>
		// TODO needs some warning if trying to get an invalid entity, and the entity
		// attributes don't actually have any validators to violate.
		public static InvoiceEntity GetEntity(bool isValid, string fixedValue = null)
		{
			if (isValid && !string.IsNullOrEmpty(fixedValue))
			{
				return GetValidEntity(fixedValue);
			}
			return isValid ? GetValidEntity() : GetInvalidEntity();
		}

		public static InvoiceEntity GetInvalidEntity()
		{
			var invoiceEntity = new InvoiceEntity
			{
				// not defining Id
				// not defining Invoicenumber
			};
			return invoiceEntity;
		}

		/// <summary>
		/// Created parents entities and set the association id's of this entity
		/// to those of the created parents.
		/// </summary>
		private void SetValidEntityAssociations()
		{
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		private void SetValidEntityAttributes()
		{
			// % protected region % [Override generated entity attributes here] off begin
			Id = DataUtils.RandInt();
			Invoicenumber = DataUtils.RandString();
			Invoicetotal = DataUtils.RandDouble();
			// % protected region % [Override generated entity attributes here] end
		}

		/// <summary>
		/// Gets an entity with attributes that conform to any attribute validators.
		/// </summary>
		public static InvoiceEntity GetValidEntity(string fixedStrValue = null)
		{
			return new InvoiceEntity
			{
				Id = DataUtils.RandInt(),
				Invoicenumber = (!string.IsNullOrWhiteSpace(fixedStrValue) && fixedStrValue.Length > 0 && fixedStrValue.Length <= 255) ? fixedStrValue : DataUtils.RandString(),
				Invoicetotal = DataUtils.RandDouble(),
			};
		}

		public override Guid Save()
		{
			return SaveToDB<Test.Models.InvoiceEntity>(InvoiceEntityDto.Convert(this));
		}
	}
}
