import React from 'react';
import Validate2 from './ValidateInfo2';
import UseForm from './UseForm';
import './Form.css';

const FormLogin = ({ submitForm }) => {
    const { handleChange, handleSubmit, values, errors } = UseForm(
        submitForm,
        Validate2
    );

    return (
        <div className='form'>
            <form onSubmit={handleSubmit} className='form' noValidate>
                <h1>
                    We are glad to see you!
                </h1>
                <div className='form-inputs'>
                    <label className='form-label'>Username</label>
                    <input
                        className='form-input'
                        type='text'
                        name='username'
                        placeholder='Enter your username'
                        value={values.username}
                        onChange={handleChange}
                    />
                    {errors.username && <p>{errors.username} </p>}
                </div>
                <div className='form-inputs'>
                    <label className='form-label'>Password</label>
                    <input
                        className='form-input'
                        type='password'
                        name='password'
                        placeholder='Enter your password'
                        value={values.password}
                        onChange={handleChange}
                    />
                    {errors.password && <p>{errors.password}</p>}
                </div>
                <button className='form-input-btn' type='login'>
                    Login
                </button>
                <span className='form-input-login'>
                    Don't have an account? Sign-up <a href="/Sign-up">here</a>
                </span>
            </form>
        </div>
    );
};

export default FormLogin;