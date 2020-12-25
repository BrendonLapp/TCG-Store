import React from 'react';

class GameDetails extends React.Component {
    constructor(props) {
        super(props);

        //this.onDelete = this.onDelete.bind(this);
    }

    // onDelete(e) { 
    //     console.log(e.target.value);
    // }

    render() {
        return (
            <div>
                <div>
                    {this.props.GameID}
                </div>
                <div>
                    {this.props.GameName}
                </div>
                <div>
                    <button onClick={this.onDelete} value={this.props.GameID}>Delete</button>
                </div>
            </div>
        )
    }
}

export default GameDetails;