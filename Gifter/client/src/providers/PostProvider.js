import React, { useState } from "react";

export const PostContext = React.createContext();

export const PostProvider = (props) => {
    const [posts, setPosts] = useState([]);

    const getAllPosts = () => {
        return fetch("api/post")
            .then((res) => res.json())
            .then(setPosts);
    };

    const getPost = (id) => {
        return fetch(`/api/post/${id}`).then((res) => res.json());
    };

    const getUserPosts = (id) => {
        console.log(id)
        return fetch(`/api/post/getbyuser/${id}`).then((res) => res.json()).then(setPosts);
    };

    const addPost = (post) => {
        return fetch("/api/post", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(post),
        });
    };

    const searchPosts = (search, sortDesc = true) => {
        return fetch("api/post/search?q=" + search + "&sortDesc=" + sortDesc)
            .then((res) => res.json())
            .then(setPosts);
    };

    return (
        <PostContext.Provider value={{ posts, getAllPosts, getPost, addPost, searchPosts, getUserPosts }}>
            {props.children}
        </PostContext.Provider>
    );
};