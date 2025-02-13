export function NumberFormInput({ name, label, value, handleChange }) {
    return (
        <div className="form-group mt-2">
            <label className="form-label" htmlFor={name}>{label}</label>
            <input className="form-control" id={name} type="number" name={name} value={value} onChange={handleChange} required min={0} step={0.01} placeholder="Např. 1000.50" />
            <span className="error-message" id={`${name}-error`}></span>
        </div>
    )
}