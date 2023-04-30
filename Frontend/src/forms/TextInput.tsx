import { FieldValidator } from "final-form";
import { Field, useField } from "react-final-form";
import { required } from "../utils/FormValidationRules";

type Props = {
	id: string;
	validate?: FieldValidator<string>;
	label: string;
};

const TextInput = ({ id, validate }: Props) => {
	const { meta } = useField(id, {
		subscription: { value: true, touched: true, error: true },
		validate,
	});
	const hasError = meta.touched && meta.error;
	return (
		<>
			<Field
				name="email"
				component="input"
				placeholder="Email"
				validate={required}
			/>
			{hasError && <span>{meta.error}</span>}
		</>
	);
};

export default TextInput;
