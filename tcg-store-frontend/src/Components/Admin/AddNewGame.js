const AddNewGame = props => {
    return (
        <form onSubmit={props.AddNewGame}>
            <div>
                <div>
                    <label>Game Name</label>
                    <input type="text" name="gameName" value={props.gameName} onChange={props.handleInputChange}></input>
                </div>
            </div>
            <button type="submit">Submit</button>
        </form>
    )
}

export default AddNewGame;