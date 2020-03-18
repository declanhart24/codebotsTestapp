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
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test.Helpers;
using Test.Models;
using Test.Services;
using Test.Services.Interfaces;
using Test.Utility;
using GraphQL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Test.Controllers.Entities
{
	/// <summary>
	/// The controller that provides rest endpoints for the InvoiceEntity model
	/// </summary>
	[Route("/api/entity/InvoiceEntity")]
	[Authorize]
	[ApiController]
	public class InvoiceEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;
		// % protected region % [Add any extra class variables here] off begin
		// % protected region % [Add any extra class variables here] end

		public InvoiceEntityController(
			ICrudService crudService
			// % protected region % [Add any extra constructor arguments here] off begin
			// % protected region % [Add any extra constructor arguments here] end
			)
		{
			_crudService = crudService;
			// % protected region % [Add any extra constructor logic here] off begin
			// % protected region % [Add any extra constructor logic here] end
		}

		/// <summary>
		/// Get the InvoiceEntity for the given id
		/// </summary>
		/// <param name="id">The id of the InvoiceEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The InvoiceEntity object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<InvoiceEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<InvoiceEntity>(id);
			return await result
				.Select(model => new InvoiceEntityDto(model))
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all InvoiceEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of InvoiceEntitys</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<IEnumerable<InvoiceEntityDto>> Get([FromQuery]InvoiceEntityOptions options, CancellationToken cancellation)
		{
			var result = _crudService.Get<InvoiceEntity>(pagination: new Pagination(options));
			return await result
				.Select(model => new InvoiceEntityDto(model))
				.ToListAsync(cancellation);
		}

		/// <summary>
		/// Create InvoiceEntity
		/// </summary>
		/// <param name="model">The new InvoiceEntity to be created</param>
		/// <returns>The InvoiceEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Authorize]
		public async Task<InvoiceEntityDto> Post([BindRequired, FromBody] InvoiceEntityDto model)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new InvoiceEntityDto((await _crudService.Create(new List<InvoiceEntity>{model.ToModel()})).FirstOrDefault());
		}

		/// <summary>
		/// Update an InvoiceEntity
		/// </summary>
		/// <param name="model">The InvoiceEntity to be updated</param>
		/// <returns>The InvoiceEntity object after it has been updated</returns>
		[HttpPut]
		[Authorize]
		public async Task<InvoiceEntityDto> Put([BindRequired, FromBody] InvoiceEntityDto model)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new InvoiceEntityDto((await _crudService.Update(new List<InvoiceEntity>{model.ToModel()})).FirstOrDefault());
		}

		/// <summary>
		/// Deletes a InvoiceEntity
		/// </summary>
		/// <param name="id">The id of the InvoiceEntity to delete</param>
		/// <returns>The ids of the deleted InvoiceEntitys</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id)
		{
			return (await _crudService.Delete<InvoiceEntity>(new List<Guid> {id})).FirstOrDefault();
		}

		/// <summary>
		/// Export the list of invoices with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of invoices</returns>
		[HttpGet]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task Export([FromQuery]IEnumerable<WhereExpression> conditions, CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<InvoiceEntity>()
				.AsNoTracking()
				.AddWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new InvoiceEntityDto(r)),
				"export_invoice.csv",
				cancellationToken);
		}

		/// <summary>
		/// Export a list of invoices with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of invoices</returns>
		[HttpPost]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task ExportPost([FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions, CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<InvoiceEntity>()
				.AsNoTracking()
				.AddConditionalWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new InvoiceEntityDto(r)),
				"export_invoice.csv",
				cancellationToken);
		}

		public class InvoiceEntityOptions : PaginationOptions
		{
			// % protected region % [Add any get params here] off begin
			// % protected region % [Add any get params here] end
		}

		// % protected region % [Add any further endpoints here] off begin
		// % protected region % [Add any further endpoints here] end
	}
}
