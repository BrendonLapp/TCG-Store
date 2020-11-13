
import './App.css';
import AddNewGame from './Components/Admin/AddNewGame';
import GameList from './Components/Admin/GameList';
import React from 'react';
import axios from 'axios';
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

  addNewGame(event) {
    event.preventDefault();

    // let axiosConfig = {
    //   headers: {
    //     'Content-Type': 'application/json;charset=UTF-8'
    //   }
    // }

    
    const newGame = {
      gameName: this.state.gameName
    };
    console.log(newGame);

    axios.post('http://localhost:44314/api/game', 
    newGame )
      .then(res => {
        console.log(res);
        console.log(res.data);
      })

  }

  render() {
    return (
      <div>

        <div>
          <AddNewGame
            gameName = {this.state.gameName}
            handleInputChange = {this.handleInputChange}
            addNewGame = {this.addNewGame}
          />
        </div>
        {/* <div>
          <GameList/>
        </div> */}
      </div>
    )
  }

}



export default App;
