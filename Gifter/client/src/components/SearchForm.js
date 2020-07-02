import React, { useContext, useRef } from "react";
import { Form, FormGroup, Label, Input, Button, Card, CardBody, CardHeader } from "reactstrap";
import { PostContext } from "../providers/PostProvider";

export const SearchForm = () => {

    const { searchPosts, getAllPosts } = useContext(PostContext);

    const search = useRef();

    const constructNewSearch = () => {
        if (search.current.value === "") {
            getAllPosts();
        } else {
            searchPosts(search.current.value);
        }
    }

    return (
        <Card className="m-4">
            <CardBody className="text-left">
                <Form>
                    <FormGroup>
                        <Input
                            type="text"
                            name="search"
                            id="search"
                            innerRef={search}
                            placeholder="Search!"
                            onChange={
                                evt => {
                                    constructNewSearch()
                                }
                            }
                        />
                    </FormGroup>
                </Form>
            </CardBody>
        </Card>
    )

}