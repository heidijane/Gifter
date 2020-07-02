import React, { useState } from "react";

export const PostContext = React.createContext();

export const PostProvider = (props) => {
    const [posts, setPosts] = useState([]);

    const getAllPosts = () => {
        return fetch("api/post")
            .then((res) => res.json())
            .then(setPosts);
    };

    const addPost = (post) => {
        return fetch("api/post", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(post),
        }).then(getAllPosts);
    };

    const searchPosts = (search, sortDesc = true) => {
        console.log(search);
        return fetch("api/post/search?q=" + search + "&sortDesc=" + sortDesc)
            .then((res) => res.json())
            .then(setPosts);
    };

    return (
        <PostContext.Provider value={{ posts, getAllPosts, addPost, searchPosts }}>
            {props.children}
        </PostContext.Provider>
    );
};