import React from 'react';

//Components
import Game from './Game';

class Admin extends React.Component {
    render() {
        return (
            <div>
                <h1 className="alert alert-light">Admin Page</h1>
                <Game />
            </div>
        );
    }
}

export default Admin;