import React from 'react';

import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';

import {usePermission} from '../Context/Permission.context'

const PermissionTable = () => {
  
  const permissionContext = usePermission();
  const [dataTable, setDataTable] = React.useState([])

  React.useEffect(() => {
    
    if (permissionContext?.permissions) {
      const newData = permissionContext.permissions.map(item =>
        createData(item.id, item.nameEmployee, item.lastNameEmployee, item.permissionTypeId, item.date)
      );
      // Actualiza el estado con el nuevo array
      setDataTable(newData);
    }
  }, [permissionContext?.permissions]);

  function createData(id, nameEmployee, lastNameEmployee, permissionTypeId, date) {
    return { id, nameEmployee, lastNameEmployee, permissionTypeId, date };
  }

  return (
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 650 }} md={{maxHeight: 550 }} aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell>Id</TableCell>
            <TableCell align="right">Nombre</TableCell>
            <TableCell align="right">Apellido</TableCell>
            <TableCell align="right">Tipo Permiso</TableCell>
            <TableCell align="right">Fecha</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {dataTable?.map((row , index) => (
            <TableRow
              key={index}
              sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
              <TableCell component="th" scope="row">
                {row.id}
              </TableCell>
              <TableCell align='right'>{row.nameEmployee}</TableCell>
              <TableCell align="right">{row.lastNameEmployee}</TableCell>
              <TableCell align="right">{row.permissionTypeId}</TableCell>
              <TableCell align="right">{row.date.toString()}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}

export default PermissionTable;