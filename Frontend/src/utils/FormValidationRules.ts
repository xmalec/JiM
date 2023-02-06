import { FieldValidator } from "final-form";

export const required: FieldValidator<string> = (value?: string) =>
	value ? undefined : "Required";

export const phoneNumber: FieldValidator<string> = (value?: string) =>
	!value || /[^a-z]/.test(value) ? undefined : "Not a phone number!";

export const specialCharacter: FieldValidator<string> = (value?: string) =>
	!value || /[^A-Za-z0-9]/.test(value)
		? undefined
		: "Missing special character!";

export const minLenght: (min: number) => FieldValidator<string> =
	(min: number) => (value?: string) =>
		!value || value.length >= min
			? undefined
			: `Password is too short. Minimum length is ${min}`;

export const composeValidators =
	(...validators: FieldValidator<string>[]) =>
	(value: string, allValues: object) =>
		validators.reduce(
			(error, validator) => error ?? validator(value, allValues),
			undefined
		);

export const strongPassword: FieldValidator<string> = composeValidators(
	minLenght(8),
	required,
	specialCharacter
);
