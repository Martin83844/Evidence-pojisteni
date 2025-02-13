export function AddFormInput({ name, label, handleChange, placeholder, pattern, type, value }) {
  return (
    <div className="form-group mt-2">
      <label className="form-label" htmlFor={name}>{label}</label>
      <input className="form-control" id={name} type={type} name={name} onChange={handleChange} placeholder={placeholder} required pattern={pattern} value={value} />
      <span className="error-message" id={`${name}-error`}></span>
    </div>
  )
}