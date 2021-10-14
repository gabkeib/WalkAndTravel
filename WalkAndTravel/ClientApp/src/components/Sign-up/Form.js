import React, { useState } from 'react';
import './Form.css';
import FormSignup from './FormSignup';
import FormSuccess from './FormSuccess';
import { Link } from 'react-router-dom';
import {NavLink } from 'reactstrap';

const Form = () => {
    const [isSubmitted, setIsSubmitted] = useState(false);

    function submitForm() {
        setIsSubmitted(true);
    }
    return (
        <>
            <div className='form-container'>
                <NavLink tag={Link} className='close-btn' to="/">X</NavLink>
                {!isSubmitted ?
                    (<FormSignup submitForm={submitForm} />
                    ):
                    (<FormSuccess />) 
                    }
            </div>
        </>
    );
};

export default Form;