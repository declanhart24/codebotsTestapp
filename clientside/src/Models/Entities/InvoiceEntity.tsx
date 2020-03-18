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
import _ from 'lodash';
import moment from 'moment';
import { observable, runInAction } from 'mobx';
import { IAttributeGroup, Model, IModelAttributes, attribute, entity, jsonReplacerFn } from 'Models/Model';
import * as Validators from 'Validators';
import * as Models from '../Entities';
import { CRUD } from '../CRUDOptions';
import * as AttrUtils from "Util/AttributeUtils";
import { IAcl } from 'Models/Security/IAcl';
import { makeFetchManyToManyFunc, makeFetchOneToManyFunc, makeJoinEqualsFunc, makeEnumFetchFunction } from 'Util/EntityUtils';
import { IOrderByCondition } from 'Views/Components/ModelCollection/ModelQuery';
import { EntityFormMode } from 'Views/Components/Helpers/Common';
// % protected region % [Add any further imports here] off begin
// % protected region % [Add any further imports here] end

export interface IInvoiceEntityAttributes extends IModelAttributes {
	id: number;
	invoicenumber: string;

	// % protected region % [Add any custom attributes to the interface here] off begin
	// % protected region % [Add any custom attributes to the interface here] end
}

// % protected region % [Customise your entity metadata here] off begin
@entity('InvoiceEntity', 'invoice')
// % protected region % [Customise your entity metadata here] end
export default class InvoiceEntity extends Model implements IInvoiceEntityAttributes {
	public static acls: IAcl[] = [
		// % protected region % [Add any further ACL entries here] off begin
		// % protected region % [Add any further ACL entries here] end
	];

	@Validators.Required()
	@Validators.Integer()
	@Validators.Unique()
	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for attribute 'ID'] off begin
		name: 'ID',
		displayType: 'textfield',
		headerColumn: true,
		searchable: true,
		searchFunction: 'equal',
		searchTransform: AttrUtils.standardiseInteger,
		// % protected region % [Modify props to the crud options here for attribute 'ID'] end
	})
	public id: number;

	/* invoice number */
	@Validators.Required()
	@observable
	@attribute()
	@CRUD({
		// % protected region % [Modify props to the crud options here for attribute 'InvoiceNumber'] off begin
		name: 'InvoiceNumber',
		displayType: 'textfield',
		headerColumn: true,
		searchable: true,
		searchFunction: 'like',
		searchTransform: AttrUtils.standardiseString,
		// % protected region % [Modify props to the crud options here for attribute 'InvoiceNumber'] end
	})
	public invoicenumber: string;

	// % protected region % [Add any custom attributes to the model here] off begin
	// % protected region % [Add any custom attributes to the model here] end

	constructor(attributes?: Partial<IInvoiceEntityAttributes>) {
		// % protected region % [Add any extra constructor logic before calling super here] off begin
		// % protected region % [Add any extra constructor logic before calling super here] end

		super(attributes);

		// % protected region % [Add any extra constructor logic after calling super here] off begin
		// % protected region % [Add any extra constructor logic after calling super here] end
	}

	public assignAttributes(attributes?: Partial<IInvoiceEntityAttributes>) {
		super.assignAttributes(attributes);

		if (attributes) {
			if (attributes.id) {
				this.id = attributes.id;
			}
			if (attributes.invoicenumber) {
				this.invoicenumber = attributes.invoicenumber;
			}
		}
	}

	// % protected region % [Customize Default Expands here] off begin
	public defaultExpands = `
	`;
	// % protected region % [Customize Default Expands here] end


	public async saveFromCrud(formMode: EntityFormMode) {
		// % protected region % [Customize Save From Crud here] off begin
		const relationPath = {
		};
		return this.save(
			relationPath,
			{
				options: [
					{
						key: 'mergeReferences',
						graphQlType: '[String]',
						value: [
						]
					},
				],
			});
		// % protected region % [Customize Save From Crud here] end
	}

	public getDisplayName() {
		// % protected region % [Customise the display name for this entity] off begin
		return this.id;
		// % protected region % [Customise the display name for this entity] end
	}

	// % protected region % [Add any further custom model features here] off begin
	// % protected region % [Add any further custom model features here] end
}