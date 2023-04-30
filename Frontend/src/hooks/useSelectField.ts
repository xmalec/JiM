import { useCallback, useState } from "react";

const useSelectField = (id: string, required?: boolean) => {
	const [value] = useState("");
	const [touched, setTouched] = useState(false);

	const error = required && touched && !value;

	return [
		// Current value for convenient access
		value,
		// Props for the TextField
		{
			id,
			value,
			// onChange: useCallback((e: SelectChangeEvent) => {
			// 	setValue(e.target.value);
			// }, []),
			onBlur: useCallback(() => setTouched(true), []),
			required,
			error,
			helperText: error ? "Required" : undefined,
		},
	] as const;
};
export default useSelectField;
