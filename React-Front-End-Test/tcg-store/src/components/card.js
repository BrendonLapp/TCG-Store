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
                        <p>{card.atk}</p>
                        <p>{card.def}</p>
                        <p>{card.linkval}</p>
                    </div>
                )
                )
            )
        )
    }
}

export default Card