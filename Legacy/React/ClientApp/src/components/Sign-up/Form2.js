import React, { useState } from 'react';
import './Form.css';
import FormLogin from './FormLogin';
import { Link } from 'react-router-dom';
import { NavLink } from 'reactstrap';
import Home from '../Home';



const Form2 = () => {
    const [isSubmitted, setIsSubmitted] = useState(false);

    function submitForm() {
        setIsSubmitted(true);
    }
    return (
        <>
            
        {!isSubmitted ?
            (<div className='form-container'>
                <NavLink tag={Link} className='close-btn' to="/">X</NavLink>
                <FormLogin submitForm={submitForm} />
                </div>)
            : (
            <div>
                <Home/>
            </div>)
        }
            
        </>
    );
};

export default Form2;