import React from 'react';
import axios from 'axios';
//Components
import AddGame from './AddGame';
import GameList from './GameList';

class Game extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            GameList: [],
            GameName: ''
        }
        this.getGames = this.getGames.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onChange = this.onChange.bind(this);
        this.onSubmit = this.onSubmit.bind(this);
    }

    componentDidMount() {
        this.getGames();
        this.setState((state) => {
            return{
                GameName: ""
            };
        })
    };

    getGames() {
        axios.get('https://localhost:44314/api/v1/Games')
        .then(res => {
            console.log(res);
            this.setState({GameList: res.data})
        })
    }

    onDelete(GameID, e) {
        axios.delete('https://localhost:44314/api/v1/Games/' + GameID)
        .then(res => {
            this.getGames()
        })
    }

    onChange(e) {
        this.setState({GameName: e.target.value});
    }

    onSubmit(e) {
        axios.post('https://localhost:44314/api/v1/Games', {
            GameName: this.state.GameName
        })
        .then(res => {
            console.log(res);
            this.getGames();
        })

        e.preventDefault();
    }

    render() {
        return (
            <div>
                <AddGame 
                    GameName={this.state.GameName}
                    onChange={this.onChange}
                    onSubmit={this.onSubmit}
                    />
                <GameList 
                    GameList={this.state.GameList}
                    onDelete={this.onDelete}
                />
            </div>
        );
    }
}

export default Game;