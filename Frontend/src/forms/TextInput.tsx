import { FieldValidator } from "final-form";
import { useField } from "react-final-form";

type Props = {
	id: string;
	validate?: FieldValidator<string>;
	label: string;
};

const TextInput = ({ id, validate, label }: Props) => {
	const { input, meta } = useField(id, {
		subscription: { value: true, touched: true, error: true },
		validate,
	});
	const hasError = meta.touched && meta.error;
	return (
		<>
			<label>{label}</label>
			<input type="text" {...input} />
		</>
	);
};

export default TextInput;
