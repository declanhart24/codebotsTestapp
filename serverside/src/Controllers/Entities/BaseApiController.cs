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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Test.Utility;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

namespace Test.Controllers.Entities
{
	[ApiController]
	[Authorize]
	public class BaseApiController : Controller
	{
		/// <summary>
		/// Sets the headers for downloading a file
		/// </summary>
		/// <param name="contentType">The content type to download</param>
		/// <param name="fileName">The name of the file to download</param>
		protected void SetDownloadFileHeaders(string contentType, string fileName)
		{
			Response.Headers["Content-Type"] = contentType;
			Response.Headers["Content-Disposition"] = $"attachment; filename=\"{fileName}\"";
		}

		/// <summary>
		/// Writes an IQueryable to a the output stream of the response as a csv
		/// </summary>
		/// <param name="queryable">The queryable to write</param>
		/// <param name="fileName">The file name to write out</param>
		/// <param name="cancellationToken">Cancellation token to cancel the action</param>
		/// <typeparam name="T">The type of the entity to write out</typeparam>
		/// <returns></returns>
		protected async Task WriteQueryableCsvAsync<T>(
			IQueryable<T> queryable,
			string fileName,
			CancellationToken cancellationToken = default)
		{
			SetDownloadFileHeaders("text/csv", fileName);
			await using var writer = new StreamWriter(Response.Body);
			await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

			csv.SetIsoDateTimeFormat();

			csv.WriteHeader<T>();
			await csv.NextRecordAsync();
			await csv.WriteQueryableAsync(queryable, cancellationToken);
		}

		// % protected region % [Add any extra functions here] off begin
		// % protected region % [Add any extra functions here] end
	}
}
