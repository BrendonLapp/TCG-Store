import React from 'react';
import axios from 'axios';

function AddGame(props) {
    return (
        <form onSubmit={props.onSubmit}>
            <label>
                Game Name:
                <input type="text" value={props.GameName} onChange={props.onChange}/>
            </label>
            <input type="submit" value="Submit"/>
        </form>
    )
}
export default AddGame;