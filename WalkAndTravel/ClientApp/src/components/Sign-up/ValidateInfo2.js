export default function ValidateInfo2(values) {
    let errors = {};

    //Checks if username field is empty
    if (!values.username) {
        errors.username = 'Username required';
    }

    //Checks if password field is empty
    if (!values.password) {
        errors.password = 'Password is required';
    }
    return errors;
}