import { useState } from "react";
import { Form } from "react-final-form";
import { useNavigate } from "react-router-dom";

import { register } from "../hooks/useFetchData";
import {
	composeValidators,
	phoneNumber,
	required,
} from "../utils/FormValidationRules";
import TextInput from "./TextInput";

const RegistrationForm = () => {
	const [submitError, setSubmitError] = useState<string>();
	const navigate = useNavigate();
	return (
		<Form
			onSubmit={async (values) => {
				try {
					await register(values);
					navigate("/");
				} catch (error) {
					if (error instanceof Error) setSubmitError(error?.message);
				}
			}}
			render={({ handleSubmit }) => (
				<form onSubmit={handleSubmit}>
					<TextInput
						id="name"
						label="Company name"
						validate={required}
					/>
					<TextInput
						id="phoneNumber"
						label="Contact phone number"
						validate={composeValidators(required, phoneNumber)}
					/>
					<div>
						{submitError && <span>{submitError}</span>}
						<button type="submit">Make registration</button>
					</div>
				</form>
			)}
		/>
	);
};

export default RegistrationForm;
