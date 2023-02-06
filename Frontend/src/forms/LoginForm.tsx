import { useState, FormEvent, useCallback } from "react";
import { useNavigate } from "react-router-dom";

import { login } from "../hooks/useFetchData";
import useField from "../hooks/useField";
import useLoggedInUser from "../hooks/useLoggedInUser";
import { BadRequestError } from "../utils/ApiConnector";
import { Form, Field } from "react-final-form";
import { required } from "../utils/FormValidationRules";
import { log } from "console";
import TextInput from "./TextInput";

const LoginForm = () => {
	const navigate = useNavigate();
	const [, setAuth] = useLoggedInUser();
	const onSubmit = useCallback(() => {
		console.log("submited");
	}, []);
	return (
		<Form
			onSubmit={onSubmit}
			render={({ handleSubmit }) => (
				<form onSubmit={handleSubmit}>
					<h2>Login</h2>
					<div>
						<label>First Name</label>
						<TextInput id="email" label="login"></TextInput>
					</div>

					<button type="submit">Submit</button>
				</form>
			)}
		/>
	);
};

export default LoginForm;
