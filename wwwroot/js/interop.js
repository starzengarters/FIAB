window.SetFocusToElement = (id) => {
	let element = document.getElementById(id);
	console.log(element);
	if(element) {
		element.focus();
	}
};