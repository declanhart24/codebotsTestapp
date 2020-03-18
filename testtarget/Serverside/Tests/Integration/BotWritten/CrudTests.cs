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
using Test.Controllers.Entities;
using Test.Models;
using Microsoft.Extensions.DependencyInjection;
using ServersideTests.Helpers;
using ServersideTests.Helpers.EntityFactory;
using Xunit;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace ServersideTests.Tests.Integration.BotWritten
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Unit")]
	public class CrudTests
	{
		[Fact]
		public async void InvoiceEntityControllerGetTest()
		{
			// % protected region % [Configure controller get test for invoice here] off begin
			using var host = ServerBuilder.CreateServer();

			var database = host.Services.GetRequiredService<TestDBContext>();
			var controller = host.Services.GetRequiredService<InvoiceEntityController>();

			var entities = new EntityFactory<InvoiceEntity>(10)
				.UseAttributes()
				.UseReferences()
				.UseOwner(Guid.NewGuid())
				.Generate()
				.ToList();

			database.AddRange(entities);
			await database.SaveChangesAsync();

			var data = await controller.Get(null, default);
			Assert.Contains(data, d => entities.Select(r => r.Id).Contains(d.Id));
			// % protected region % [Configure controller get test for invoice here] end
		}

		// % protected region % [Add any additional methods here] off begin
		// % protected region % [Add any additional methods here] end
	}
}