import React from 'react';
import './Form.css';

const FormSuccess = () => {
    return (
        <div className='form-correct'>
            <h1 className='form-success'>Your account has been created!</h1>
            <img className='form-img-2' src='Images/welcome.png' alt='success-image' />
            <h1 className='form-input-login'> Login <a href="/Login">here</a>
            </h1>
        </div>
    );
};

export default FormSuccess;