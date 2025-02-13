export function EditFormInput({ name, label, handleChange, placeholder, pattern, type, value, disabled = false }) {
  return (
    <div className="form-group mt-2">
      <label className="form-label" htmlFor={name}>{label}</label>
      <input className="form-control" id={name} type={type} name={name} onChange={handleChange} placeholder={placeholder} required pattern={pattern} value={value} disabled={disabled} />
      <span className="error-message" id={`${name}-error`}></span>
    </div>
  )
}