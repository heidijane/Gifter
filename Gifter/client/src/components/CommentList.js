import React from "react";
import { ListGroup, ListGroupItem } from "reactstrap";

export const CommentList = ({ comments }) => {

    return (

        <ListGroup flush className="border-top mx-n3 text-left mb-n3">
            {(comments.length > 0 ? comments.map(comment => (
                <ListGroupItem key={"comment_" + comment.id}>
                    <p>{comment.message} - {comment.userProfile.name}</p>
                </ListGroupItem>
            )) : <p className="font-italic text-center text-muted mt-3">no comments</p>)}
        </ListGroup>
    );

};