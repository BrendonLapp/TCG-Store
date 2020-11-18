
import './App.css';
import AddNewGame from './Components/Admin/AddNewGame';
import GameList from './Components/Admin/GameList';
import React from 'react';
import axios from 'axios';
import $ from 'jquery';
// import $, { post } from 'jquery';

class App extends React.Component {
  constructor() {
    super();
    this.state = {
      id: null,
      gameName: '',
      gameDetails: {}
    }

    this.handleInputChange = this.handleInputChange.bind(this);
    this.addNewGame = this.addNewGame.bind(this);
  }

  handleInputChange(event) {
    const target = event.target;
    const value = target.value;
    const name = target.name;
    
    this.setState({
      [name]:value
    })
  }

  addNewGame() {
    //event.preventDefault();
    
    const NewGame = ({
      GameName: this.state.gameName
    });
    console.log(NewGame);

    // $.ajax({
    //   url: 'https://localhost:44314/api/GameController/Post',
    //   type: "POST",
    //   contentType: 'application/json',
    //   headers: "Access-Control-Allow-Origin: *",
    //   data: NewGame,
    //   success: console.log(NewGame)
    // });
    axios.post('https://localhost:44314/api/GameController', NewGame)
  }

  render() {
    return (
      <div>

        <div>
          <AddNewGame
            gameName = {this.state.gameName}
            handleInputChange = {this.handleInputChange}
            addNewGame = {this.addNewGame()}
          />
        </div>
        {<div>
          <GameList/>
        </div>}
      </div>
    )
  }

}



export default App;
