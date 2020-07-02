import React from "react";
import "./App.css";
import { PostProvider } from "./providers/PostProvider";
import PostList from "./components/PostList";

function App() {
  return (
    <div className="App">
      <h1 className="display-3">Gifter</h1>
      <PostProvider>
        <PostList />
      </PostProvider>
    </div>
  );
}

export default App;