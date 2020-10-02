import React from 'react'

class CardDetail extends React.Component {
    render() {
        return (
            <div>
                <h1>{this.props.card.id}</h1>
            </div>
        )
    }
}
export default CardDetail