import React from "react";
import { Card, CardImg, CardBody } from "reactstrap";
import { CommentList } from "./CommentList";
import { Link } from "react-router-dom";

const Post = ({ post }) => {
    return (
        <Card className="m-4">
            <CardImg top src={post.imageUrl} alt={post.title} />
            <CardBody>
                <h3><Link to={`/posts/${post.id}`}>{post.title}</Link></h3>
                <p className="lead">{post.caption}</p>
                <p>Posted by {post.userProfile.name}</p>
                <CommentList comments={post.comments}></CommentList>
            </CardBody>
        </Card>
    );
};

export default Post;