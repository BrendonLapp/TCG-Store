import React from 'react';
import axios from 'axios';
import $ from 'jquery';
//Components
import GameDetails from './GameDetails';

function GameList(props) {
    return (
        <ul className="list-group">
            {props.GameList.map( gameDetails =>
                <li key={gameDetails.gameID} className="list-group-item"> 
                    <span>{gameDetails.gameID}</span>
                    <span>{gameDetails.gameName}</span>
                    <button onClick={(e) => props.onDelete(gameDetails.gameID, e)}>Delete</button>
                </li>
                )}
        </ul>
    )
}

export default GameList;