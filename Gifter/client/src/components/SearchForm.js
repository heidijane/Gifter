import React, { useContext, useRef } from "react";
import { Form, FormGroup, Input, Card, CardBody } from "reactstrap";
import { PostContext } from "../providers/PostProvider";

export const SearchForm = () => {

    //Prevents a function from being called too often
    //Function will only be invoked after "wait" time in milliseconds has elapsed
    const debounce = (func, wait) => {
        let timeout;

        //the function that will be executed when time has elapsed
        return function executedFunction(...args) {

            //callback function that is executed after debounce time has elapsed
            const later = () => {
                timeout = null;
                func(...args); //spread (...) operator captures parameters we want to pass to the invoked function
            };

            //The clearTimeout() method clears a timer set with the setTimeout() method.
            clearTimeout(timeout);

            //The setTimeout() method calls a function or evaluates an expression after a specified number of milliseconds.
            timeout = setTimeout(later, wait);
        };
    };

    const { searchPosts, getAllPosts } = useContext(PostContext);

    const search = useRef();

    const constructNewSearch = debounce(() => {
        if (search.current.value === "") {
            getAllPosts();
        } else {
            searchPosts(search.current.value);
        }
    }, 800);

    return (
        <Card className="m-4">
            <CardBody className="text-left bg-light">
                <Form>
                    <FormGroup>
                        <Input
                            type="text"
                            name="search"
                            id="search"
                            innerRef={search}
                            placeholder="Start typing to filter..."
                            onChange={
                                evt => {
                                    constructNewSearch();
                                }
                            }
                        />
                    </FormGroup>
                </Form>
            </CardBody>
        </Card>
    )

}