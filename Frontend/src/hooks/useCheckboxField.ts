import { useCallback, useState } from "react";

const useCheckboxField = (id: string, required = false) => {
	const [checked, setValue] = useState(true);
	const [touched, setTouched] = useState(false);

	const error = required && touched && !checked;

	return [
		// Current value for convenient access
		checked,
		// Props for the TextField
		{
			id,
			value: checked,
			onChange: useCallback(() => setValue(!checked), [checked]),
			onBlur: useCallback(() => setTouched(true), []),
			required,
			error,
			helperText: error ? "Required" : undefined
		}
	] as const;
};

export default useCheckboxField;
