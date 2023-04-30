import { useCallback } from "react";
import { Form } from "react-final-form";
import TextInput from "./TextInput";

const LoginForm = () => {
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
