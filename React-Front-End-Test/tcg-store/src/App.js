import React from 'react';
import Card from './components/card'
import axios from 'axios'

class App extends React.Component {
  state = {
    card: []
  }

  componentDidMount() {
    axios.get(`https://db.ygoprodeck.com/api/v7/cardinfo.php?name=scrap_wyvern`)
      .then(res => {
        const card = res.data.data;
        this.setState({ card });
      })
  }

  render() {
    return (
      <div>
        <Card card={this.state.card}/>
      </div>
    )
  }
}

export default App;
