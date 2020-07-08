import React, { useState, useContext } from "react";
import { UserProfileContext } from "../providers/UserProfileProvider.js";

export const PostContext = React.createContext();

export const PostProvider = (props) => {
    const [posts, setPosts] = useState([]);

    const apiUrl = "/api/post";
    const { getToken } = useContext(UserProfileContext);

    const getAllPosts = () =>
        getToken().then((token) =>
            fetch(apiUrl, {
                method: "GET",
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }).then(resp => resp.json())
                .then(setPosts));

    const getPost = (id) =>
        getToken().then((token) =>
            fetch(`/api/post/${id}`, {
                method: "GET",
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            }).then((res) => res.json())
        );

    const getUserPosts = (id) => {
        console.log(id)
        return fetch(`/api/post/getbyuser/${id}`).then((res) => res.json()).then(setPosts);
    };

    const addPost = (post) => {
        getToken().then((token) =>
            fetch(apiUrl, {
                method: "POST",
                headers: {
                    Authorization: `Bearer ${token}`,
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(post)
            }).then(resp => resp.json())
        );
    };

    const searchPosts = (search, sortDesc = true) => {
        getToken().then((token) => {
            fetch("api/post/search?q=" + search + "&sortDesc=" + sortDesc, {
                method: "GET",
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            })
                .then((res) => res.json())
                .then(setPosts);
        })
    };

    return (
        <PostContext.Provider value={{ posts, getAllPosts, getPost, addPost, searchPosts, getUserPosts }}>
            {props.children}
        </PostContext.Provider>
    );
};