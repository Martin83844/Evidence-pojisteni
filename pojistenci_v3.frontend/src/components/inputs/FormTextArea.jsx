export function FormTextArea({ name, label, value, handleChange, rows, placeholder }) {
    return (
        <div className="form-group mt-2">
            <label className="form-label" htmlFor={name}>{label}</label>
            <textarea className="form-control" rows={rows} id={name} type="text" name={name} value={value} onChange={handleChange} placeholder={placeholder} required />
            <span className="error-message" id={`${name}-error`}></span>
        </div>
    )
}