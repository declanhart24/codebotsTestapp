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
using ServersideInvoiceEntity = Test.Models.InvoiceEntity;

namespace APITests.EntityObjects.Models
{
	/// <summary>
	/// Invoice Table
	/// </summary>
	public class InvoiceEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public int? Id { get; set; }
		public String Invoicenumber { get; set; }
		public Double? Invoicetotal { get; set; }


		public InvoiceEntityDto(InvoiceEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Id = model.Id;
			Invoicenumber = model.Invoicenumber;
			Invoicetotal = model.Invoicetotal;
		}

		public InvoiceEntityDto(ServersideInvoiceEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Id = model.Id;
			Invoicenumber = model.Invoicenumber;
			Invoicetotal = model.Invoicetotal;
		}

		public InvoiceEntity GetTesttargetInvoiceEntity()
		{
			return new InvoiceEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Id = Id,
				Invoicenumber = Invoicenumber,
				Invoicetotal = Invoicetotal,
			};
		}

		public ServersideInvoiceEntity GetServersideInvoiceEntity()
		{
			return new ServersideInvoiceEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Id = Id,
				Invoicenumber = Invoicenumber,
				Invoicetotal = Invoicetotal,
			};
		}

		public static ServersideInvoiceEntity Convert(InvoiceEntity model)
		{
			var dto = new InvoiceEntityDto(model);
			return dto.GetServersideInvoiceEntity();
		}

		public static InvoiceEntity Convert(ServersideInvoiceEntity model)
		{
			var dto = new InvoiceEntityDto(model);
			return dto.GetTesttargetInvoiceEntity();
		}
	}
}