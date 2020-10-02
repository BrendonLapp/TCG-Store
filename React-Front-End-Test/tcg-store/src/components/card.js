import React from 'react'
import CardDetails from './carddetails'

class Card extends React.Component {
    render() {
        return (
            this.props.card.map(
                (card => (
                    <div>
                        <h1>{card.name}</h1>
                        <p>{card.desc}</p>
                    </div>
                )
                )
            )
        )
    }
}

export default Card