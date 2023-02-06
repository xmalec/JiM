import { useEffect } from "react";

const usePageTitle = (title: string) => {
	useEffect(() => {
		document.title = `${title} | B2B`;
	}, [title]);
};

export default usePageTitle;
