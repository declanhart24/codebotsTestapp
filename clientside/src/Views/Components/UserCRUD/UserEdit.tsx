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
import * as React from 'react';
import { observer } from 'mobx-react';
import EntityEdit from '../CRUD/EntityEdit';
import { User } from '../../../Models/Entities';
import EntityAttributeList from '../CRUD/EntityAttributeList';
import { getUser } from './UserService';
import { action, observable } from 'mobx';
import { EntityFormMode } from '../Helpers/Common';

@observer
class UserEdit extends EntityEdit<User> {
	@observable
	private user: User | null = null;

	public componentDidMount() {
		if (this.props.match.params.id == null) {
			throw new Error('Expected id of model to fetch for edit');
		}

		/* Fetch the model */
		getUser(this.props.match.params.id).then(this.updateUser);
	}

	public render() {
		const title = `Edit User`;
		const sectionClassName = "crud__create";
		const options = { title, sectionClassName };

		if(this.user == null) {
			return <div>Loading...</div>;
		}
		return <EntityAttributeList 
			{...this.props} model={this.user} 
			{...options}
			formMode={EntityFormMode.EDIT}
			modelType={User}
		/>;
	}

	@action
	private updateUser = (user: User) => {
		this.user = user;
	}
}

export default UserEdit;