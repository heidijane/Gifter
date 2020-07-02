import React, { useContext, useRef } from "react";
import { Form, FormGroup, Label, Input, Button, Card, CardBody, CardHeader } from "reactstrap";
import { PostContext } from "../providers/PostProvider";

export const PostForm = () => {

    const { addPost } = useContext(PostContext);

    const title = useRef();
    const postUrl = useRef();
    const caption = useRef();

    const constructNewPost = () => {
        addPost({
            Title: title.current.value,
            ImageUrl: postUrl.current.value,
            Caption: caption.current.value,
            UserProfileId: 1,
            DateCreated: new Date()
        });
    }

    return (
        <Card className="m-4">
            <CardHeader>
                <h3>Post .GIF!</h3>
            </CardHeader>
            <CardBody className="text-left">
                <Form>
                    <FormGroup>
                        <Label for="postTitle">Title</Label>
                        <Input type="text" name="title" id="postTitle" innerRef={title} placeholder="" />
                    </FormGroup>
                    <FormGroup>
                        <Label for="postUrl">Image URL</Label>
                        <Input type="text" name="url" id="postUrl" innerRef={postUrl} placeholder="" />
                    </FormGroup>
                    <FormGroup>
                        <Label for="postCaption">Caption</Label>
                        <Input type="text" name="url" id="postCaption" innerRef={caption} placeholder="" />
                    </FormGroup>
                    <FormGroup>
                        <Button
                            type="submit"
                            color="primary"
                            block
                            onClick={
                                evt => {
                                    evt.preventDefault() // Prevent browser from submitting the form
                                    constructNewPost()
                                }
                            }
                        >Submit</Button>
                    </FormGroup>
                </Form>
            </CardBody>
        </Card>
    )

}