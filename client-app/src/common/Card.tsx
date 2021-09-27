import React from "react";
import './Card.css';

interface Props {
    name: string;
}

function Card({name}: Props) {
    return (
        <div className="card test">
            <div className="logo test" />
            <div className="test">{name}</div>
        </div>
    )
};

export default Card;
