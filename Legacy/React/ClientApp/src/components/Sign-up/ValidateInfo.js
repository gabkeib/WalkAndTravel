export default function ValidateInfo(values) {
    let errors = {};

    //Checks if username field is empty
    if (!values.username) {
        errors.username = 'Username required';
    }

    //Checks if email field is empty
    if (!values.email) {
        errors.email = 'Email required';
    } //Checks if email has a valid format
    else if (!/\S+@\S+\.\S+/.test(values.email)) {
        errors.email = 'Email address is invalid';
    }

    //Checks if password field is empty
    if (!values.password) {
        errors.password = 'Password is required';
    } // Checks if password has at least 6 symbols
    else if (values.password.length < 6) {
        errors.password = 'Password needs to be 6 characters or more';
    }

    //Checks if password2 field is empty
    if (!values.password2) {
        errors.password2 = 'Password is required';
    }   //Checks if password2 field matches password field
    else if (values.password2 !== values.password) {
        errors.password2 = 'Passwords do not match';
    }
    return errors;
}