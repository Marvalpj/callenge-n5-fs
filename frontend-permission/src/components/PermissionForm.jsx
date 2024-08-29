import React, { useState } from 'react';
import {
  Grid,
  FormLabel,
  OutlinedInput,
  Button,
  Select,
  MenuItem,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions
} from '@mui/material';

import { usePermission } from '../Context/Permission.context'

const PermissionForm = () => {


  // contexto 
  const {
    createUpdatePermission,
    permissionsType,
    permissionFormData,
    setPermissionFormData,
    cleanPermissionFormData,
    createPermissionType
  } = usePermission()

  // new permission description
  const [newPermission, setNewPermission] = useState('');
  // modal
  const [open, setOpen] = useState(false);

  // modal metodos
  const handleClickOpen = () => {
    setOpen(true);
  };
  const handleClose = () => {
    setOpen(false);
  };
  
  const CreatePermissionTypehandle = async (event) => {
    event.preventDefault();
    await createPermissionType(newPermission)
    setNewPermission('')
    setOpen(false);
  };

  // cambia el valor de las variables de estado 
  const handleInputChange = (event) => {

    const { name, value } = event.target; // Desestructuramos el nombre y el valor del evento
    switch (name) {
      case 'nameEmployee':
        setPermissionFormData({ ...permissionFormData, nameEmployee: value });
        break;
      case 'lastNameEmployee':
        setPermissionFormData({ ...permissionFormData, lastNameEmployee: value });
        break;
      case 'permissionType':
        setPermissionFormData({ ...permissionFormData, permissionTypeId: value });
        break;
      case 'newPermission':
        setNewPermission(value);
        break;
      default:
        break;
    }
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    // Aquí puedes agregar la lógica para enviar el formulario
    await createUpdatePermission()
  };

  return (
    <Grid container>
      <form onSubmit={handleSubmit}>
        <Grid container spacing={3}>
          <Grid item xs={12} >
            <Grid container spacing={3} justifyContent={'flex-end'}>
              <Button variant="text" xs={{ marginLeft: 'auto' }} onClick={handleClickOpen}>
                Nuevo tipo de permiso
              </Button>
            </Grid>
          </Grid>
          <Grid item xs={12} >
            <FormLabel htmlFor="nameEmployee" required>
              Nombre
            </FormLabel>
            <OutlinedInput
              id="nameEmployee"
              name="nameEmployee"
              type="text"
              value={permissionFormData.nameEmployee}
              onChange={handleInputChange}
              placeholder="John"
              autoComplete="given-name"
              required
              fullWidth
            />
          </Grid>
          <Grid item xs={12} >
            <FormLabel htmlFor="lastNameEmployee" required>
              Apellido
            </FormLabel>
            <OutlinedInput
              id="lastNameEmployee"
              name="lastNameEmployee"
              value={permissionFormData.lastNameEmployee}
              onChange={handleInputChange}
              type="text"
              placeholder="Doe"
              autoComplete="family-name"
              required
              fullWidth
            />
          </Grid>
          <Grid item xs={12}>
            <FormLabel htmlFor="permissionType" required>
              Tipo de Permiso
            </FormLabel>
            <Select
              id="permissionType"
              name="permissionType"
              value={permissionFormData.permissionTypeId}
              onChange={handleInputChange}
              displayEmpty
              required
              fullWidth
            >
              <MenuItem value={0}>
                <em>Seleccione un tipo de permiso</em>
              </MenuItem>
              {
                Array.isArray(permissionsType) && permissionsType?.map((pt, index) => (
                  <MenuItem key={index} value={pt.id}>
                    {pt.description}
                  </MenuItem>
                ))
              }
            </Select>
          </Grid>
            
          <Grid item>
            <Button type="submit" variant="contained" size='large' color="primary">
              Enviar
            </Button>
          </Grid>
          <Grid item  >
            <Button 
              type="submit" 
              variant="outlined" 
              size='large' 
              color="primary"
              onClick={cleanPermissionFormData}
              >
              Limpiar
            </Button>
          </Grid>
        </Grid>
      </form>

      {/* Modal para crear nuevo tipo de permiso */}
      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>Nuevo Tipo de Permiso</DialogTitle>
        <DialogContent>
          <Grid>
            <FormLabel htmlFor="newPermission" required>
              Descripcion del permiso
            </FormLabel>
            <OutlinedInput
              id="newPermission"
              name="newPermission"
              type="text"
              placeholder="Permission"
              required
              fullWidth
              value={newPermission}
              onChange={handleInputChange}
            />
          </Grid>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose} color="primary">
            Cancelar
          </Button>
          <Button onClick={CreatePermissionTypehandle} color="primary">
            Guardar
          </Button>
        </DialogActions>
      </Dialog>

    </Grid>
  );
}

export default PermissionForm;