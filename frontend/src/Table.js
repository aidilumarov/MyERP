import React, {Component} from 'react'

class Table extends Component {
    constructor(props) {
        super(props)
        this.state = {
            users: [
                { userId: 1, DateRegistration: "2021-02-15", DateLastActivity: "2021-02-19"}
            ]
        }
    }
    
    renderTableData() {
        return this.state.users.map((user, index) => {
            const { userId, DateRegistration, DateLastActivity} = user
            return (
                <tr key={userId}>
                    <td>{userId}</td>
                    <td>{DateRegistration}</td>
                    <td>{DateLastActivity}</td>
                </tr>
            )
        })
    }

    render() {
        return(
        <div class="centered">
            <table id="users">
                <caption>User Data</caption>
                <thead>
                    <tr>
                        <th>UserID</th>
                        <th>Date Registration</th>
                        <th>Date Last Activity</th>
                    </tr>
                </thead>
                <tbody>
                    {this.renderTableData()}
                </tbody>
            </table>
        </div>
        );
    }
}

export default Table;