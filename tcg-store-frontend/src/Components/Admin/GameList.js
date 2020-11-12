import Axios from 'axios';
import React from 'react';

export default class GameList extends React.Component {
    state = {
        games: []
    }

    componentDidMount() {
        Axios.get('https://localhost:44314/api/game').then(res => {
            //console.log(res);
            this.setState({games: res.data});
        })
    }

    render() {
        return (
            <ul>
                {this.state.games.map( game =>
                    <li key={game.gameId}>
                        {game.gameName}
                    </li>
                )}
            </ul>
        )
    }
}